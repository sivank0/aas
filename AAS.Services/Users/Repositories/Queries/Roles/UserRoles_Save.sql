INSERT INTO userroles(
	id,
	name,
	accesspolicies,
	createddatetimeutc,
	createduserid,
	isremoved
)
VALUES(
	@p_id,
	@p_name,
	@p_accesspolicies,
	@p_currentdatetimeutc,
	@p_systemuserid,
	false
)
ON CONFLICT (id) DO UPDATE SET
	name = @p_name,
	accesspolicies = @p_accesspolicies,
	modifieddatetimeutc = @p_currentdatetimeutc,
	modifieduserid = @p_systemuserid