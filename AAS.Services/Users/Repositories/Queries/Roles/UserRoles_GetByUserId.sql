SELECT * FROM userroles
WHERE id IN (
	SELECT roleid FROM userpermissions
	WHERE userid = @p_userid
)