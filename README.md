<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>SQL запрос</title>
</head>
<body>
    <h1>SQL запрос</h1>

    <h2>Задание</h2>
    <p>Необходимо написать SQL-запрос, который выведет содержимое данной таблицы отсортированным по убыванию <code>dt</code>, но с учетом того, что записи, имеющие одинаковый <code>group_id</code>, должны обязательно следовать друг за другом (также по убыванию даты) с момента попадания первой записи из группы в отбор по убыванию <code>dt</code>.</p>

    <pre>
        <code>
            WITH numbered_rows AS (
                SELECT *, 
                    ROW_NUMBER() OVER (PARTITION BY "group_id" ORDER BY "dt" DESC) AS row_num 
                FROM "test1" 
            )
            SELECT "id", "dt", "group_id" 
            FROM numbered_rows 
            ORDER BY "group_id", row_num DESC, "dt" DESC;
        </code>
    </pre>
</body>
</html>
