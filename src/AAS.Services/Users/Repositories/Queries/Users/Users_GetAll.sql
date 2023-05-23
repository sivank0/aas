SELECT *
FROM users
WHERE NOT isremoved
ORDER BY lastname, firstname, middlename