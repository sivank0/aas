INSERT INTO users(
	id,
	firstname,
	middlename,
	lastname,
	email,
	passwordhash,
	phonenumber,
	createddatetimeutc,
	createduserid,
	isremoved
)
VALUES(
	@p_id,
	@p_firstname,
	@p_middlename,
	@p_lastname,
	@p_email,
	@p_passwordhash,
	@p_phonenumber,
	@p_currentdatetimeutc,
	@p_systemuserid,
	false
)
ON CONFLICT (id) DO UPDATE SET
	firstname = @p_firstname,
	middlename = @p_middlename,
	lastname =@p_lastname,
	email = @p_email,
	passwordhash = @p_passwordhash,
	phonenumber = @p_phonenumber,
	modifieddatetimeutc = @p_currentdatetimeutc,
	modifieduserid = @p_systemuserid
