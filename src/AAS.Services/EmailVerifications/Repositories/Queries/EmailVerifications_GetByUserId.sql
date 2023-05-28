SELECT * FROM emailverifications
WHERE userid = @p_userid AND NOT isverified