SELECT *
FROM users
WHERE id = @p_id AND 
	  (@p_includeremoved = TRUE OR isremoved = FALSE)