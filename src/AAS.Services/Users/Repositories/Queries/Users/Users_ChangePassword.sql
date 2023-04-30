UPDATE users
SET passwordhash        = @p_passwordhash,
    modifieduserid      = @p_systemuserid,
    modifieddatetimeutc = @p_currentdatetimeutc
WHERE id = @p_userid;