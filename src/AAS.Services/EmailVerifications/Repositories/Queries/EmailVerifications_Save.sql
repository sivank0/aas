INSERT INTO emailverifications(
	userid,
	token,
	isverified,
	createddatetimeutc
)
VALUES(
	@p_userid,
	@p_token,
	@p_isverified,
	@p_currentdatetimeutc
) 