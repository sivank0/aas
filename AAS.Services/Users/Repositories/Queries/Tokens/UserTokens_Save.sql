INSERT INTO usertokens(
	userid,
	token,
	datetimeutc
)
VALUES(
	@p_userid,
	@p_token,
	@p_currentdatetimeutc
) 
ON CONFLICT (userid) DO UPDATE SET
	token = @p_token,
	datetimeutc = @p_currentdatetimeutc