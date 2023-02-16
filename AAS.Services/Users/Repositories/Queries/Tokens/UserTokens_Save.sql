INSERT INTO usertokens(
	userid,
	token,
	createddatetimeutc
)
VALUES(
	@p_userid,
	@p_token,
	@p_currentdatetimeutc
) 