/* NOTE: Selects top N rows */

SELECT *
FROM [glaspoduzetnikah_clanovi_db].[glaspoduzetnikah_idea].[members]
WHERE oib_subject='01591544523'

DELETE glaspoduzetnikah_clanovi_db.glaspoduzetnikah_idea.members
WHERE oib_subject='01591544523'

SELECT count(*), oib_subject
FROM members
WHERE oib_subject IS NOT NULL
GROUP BY oib_subject
HAVING count(*) > 1

UPDATE [glaspoduzetnikah_clanovi_db].[glaspoduzetnikah_idea].[members]
SET oib_subject = RIGHT('0000' + oib_subject, 11)
WHERE len(oib_subject) < 11