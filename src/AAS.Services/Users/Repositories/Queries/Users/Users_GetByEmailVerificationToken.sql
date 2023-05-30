SELECT 
	u.id,
	u.firstname,
	u.middlename,
	u.lastname,
	u.email,
	u.passwordhash,
	u.phonenumber,
	u.createddatetimeutc AS usercreateddatetimeutc,
	u.createduserid,
	u.modifieddatetimeutc,
	u.isremoved,
	u.photopath,
	ev.token,
	ev.isverified,
	ev.createddatetimeutc AS emailverificatoncreateddatetimeutc
FROM users u 
JOIN emailverifications ev ON u.id = ev.userid
WHERE ev.token = @p_emailverificationtoken AND NOT u.isremoved