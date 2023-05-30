UPDATE emailverifications
	set isverified = true
WHERE userid = @p_userid AND token = @p_token