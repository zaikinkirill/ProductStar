--Напишите SQL-скрипт, который потокобезопасно в рамках транзакции создает новое бронирование. 
BEGIN;
DO $$
DECLARE flight_id_v integer;
begin
LOCK TABLE bookings.bookings IN ROW EXCLUSIVE MODE;
INSERT INTO bookings.bookings(book_ref, book_date, total_amount) VALUES ('00154E', '2024-07-14', 50000);


LOCK TABLE bookings.tickets IN ROW EXCLUSIVE MODE;
INSERT INTO bookings.tickets(ticket_no, book_ref, passenger_id, passenger_name, contact_data)
VALUES ('0005432340284', '00154E', '5314 505050', 'KIRILL ZAIKIN', '{"phone": "+7270060456"}');

SELECT flight_id INTO flight_id_v FROM bookings.flights WHERE flight_no = 'PG0403' FOR SHARE;

LOCK TABLE bookings.ticket_flights IN ROW EXCLUSIVE MODE;
INSERT INTO bookings.ticket_flights(ticket_no, flight_id, fare_conditions, amount)
VALUES ('0005432340284', flight_id_v, 'Economy', 50000);

EXCEPTION
		WHEN unique_violation THEN
			RAISE NOTICE 'Transaction failed: %', SQLERRM;
            -- Прерываем выполнение блока SQL
            RETURN;
        WHEN OTHERS THEN
            -- Выводим сообщение об ошибке
            RAISE NOTICE 'Transaction failed: %', SQLERRM;
            -- Прерываем выполнение блока SQL
            RETURN;

END $$;
COMMIT;



--TASK-4:поиск по полному известному имени

EXPLAIN ANALYZE
SELECT * FROM bookings.tickets
WHERE passenger_name = 'ANDREY SIDOROV';
/*
"Gather  (cost=1000.00..65830.24 rows=264 width=104) (actual time=0.413..641.768 rows=704 loops=1)"
"  Workers Planned: 2"
"  Workers Launched: 2"
"  ->  Parallel Seq Scan on tickets  (cost=0.00..64803.84 rows=110 width=104) (actual time=1.656..570.731 rows=235 loops=3)"
"        Filter: (passenger_name = 'ANDREY SIDOROV'::text)"
"        Rows Removed by Filter: 983051"
"Planning Time: 0.099 ms"
"Execution Time: 641.958 ms"
*/

CREATE INDEX idx_passenger_name ON bookings.tickets using HASH(passenger_name);

EXPLAIN ANALYZE
SELECT * FROM bookings.tickets
WHERE passenger_name = 'ANDREY SIDOROV';

/*
"Bitmap Heap Scan on tickets  (cost=10.05..1011.47 rows=264 width=104) (actual time=0.147..0.612 rows=704 loops=1)"
"  Recheck Cond: (passenger_name = 'ANDREY SIDOROV'::text)"
"  Heap Blocks: exact=695"
"  ->  Bitmap Index Scan on idx_passenger_name  (cost=0.00..9.98 rows=264 width=0) (actual time=0.075..0.075 rows=704 loops=1)"
"        Index Cond: (passenger_name = 'ANDREY SIDOROV'::text)"
"Planning Time: 0.295 ms"
"Execution Time: 0.656 ms"
*/
