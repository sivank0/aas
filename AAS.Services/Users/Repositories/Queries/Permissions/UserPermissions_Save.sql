INSERT INTO userpermissions(
	userid,
	roleid
)
VALUES(
	@p_userid,
	@p_userroleid
)
ON CONFLICT (userid) DO UPDATE SET
	roleid = @p_userroleid