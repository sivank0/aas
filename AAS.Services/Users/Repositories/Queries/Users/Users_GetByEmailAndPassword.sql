SELECT *
FROM users
WHERE email = @p_email
  AND (passwordhash = @p_passwordhash OR @p_passwordhash IS NULL)