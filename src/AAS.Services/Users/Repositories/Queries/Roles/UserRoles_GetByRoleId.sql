SELECT *
FROM userroles
WHERE id = @p_userroleid
  AND NOT isremoved