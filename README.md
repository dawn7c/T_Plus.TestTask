<h1>№1 SQL запрос</h1>
<h2>Задание</h2>
<body></body>Необходимо написать SQL-запрос, который выведет содержимое данной таблицы отсортированным по убыванию dt, но с учетом того, что записи, имеющие одинаковый group_id, должны обязательно следовать друг за другом (также по убыванию даты) с момента попадания первой записи из группы в отбор по убыванию dt.</body>
 WITH numbered_rows AS ( SELECT *, ROW_NUMBER() OVER (PARTITION BY "group_id" ORDER BY "dt" DESC) AS row_num FROM "test1" ) SELECT "id", "dt", "group_id" FROM numbered_rows ORDER BY "group_id", row_num DESC, "dt" DESC;
