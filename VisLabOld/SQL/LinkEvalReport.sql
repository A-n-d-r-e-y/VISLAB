--Link	Lane	center.x	center.y	speed
--1	    1	    195,592	    589,018	    51,9726462142718
--1	    2	    198,676	    587,363	    77,1365810869951


USE VISSIM;

------------------------------------ Links and Lanes
WITH RankedLink AS (
		SELECT ROW_NUMBER() OVER (ORDER BY Link, Lane, SegEndC) [Number], *
		FROM dbo.Olaine_LINK_EVAL),
	MiddleLink AS (
		SELECT AVG(Number) [MiddleLink]
		FROM RankedLink
		GROUP BY Link, Lane),
	AvgSpeed AS (
		SELECT AVG(v__0_) [speed], Link, Lane
		FROM dbo.Olaine_LINK_EVAL
		GROUP BY Link, Lane)

SELECT RankedLink.Link, RankedLink.Lane, (SegStX + SegEndX)/2 [center.x], (SegStY + SegEndY)/2 [center.y], AvgSpeed.speed
FROM RankedLink
	JOIN MiddleLink ON RankedLink.Number=MiddleLink.MiddleLink
	JOIN AvgSpeed ON RankedLink.Lane=AvgSpeed.Lane
		AND RankedLink.Link=AvgSpeed.Link

------------------------------------ Link only
WITH RankedLink AS (
		SELECT ROW_NUMBER() OVER (ORDER BY Link, SegEndC) [Number], *
		FROM dbo.Olaine_LINK_EVAL),
	MiddleLink AS (
		SELECT AVG(Number) [MiddleLink]
		FROM RankedLink
		GROUP BY Link),
	AvgSpeed AS (
		SELECT AVG(v__0_) [speed], Link
		FROM dbo.Olaine_LINK_EVAL
		GROUP BY Link)

SELECT RankedLink.Link, 0 [Lane], (SegStX + SegEndX)/2 [center.x], (SegStY + SegEndY)/2 [center.y], AvgSpeed.speed
FROM RankedLink
	JOIN MiddleLink ON RankedLink.Number=MiddleLink.MiddleLink
	JOIN AvgSpeed ON RankedLink.Link=AvgSpeed.Link

------------------------------------------------------------------------------------------------------------------------------

--Link	Lane    f.x	    f.y	    s.x	    s.y
--1	    1	    293,93	777,73	293,93	760,09
--1	    1	    289,14	768,87	289,14	751,32
--1	    1	    284,34	760,09	284,34	742,55

SELECT f.Link, f.Lane, f.SegEndX [f.x], f.SegStY [f.y], s.SegStX [s.x], s.SegEndY [s.y]
FROM dbo.Olaine_LINK_EVAL f
JOIN dbo.Olaine_LINK_EVAL s ON f.SegEndX = s.SegStX
	AND f.Link=s.Link
	AND f.Lane=s.Lane