/* NOTE: Removes all offer content items */

DECLARE @idPattern nvarchar(50)
SET @idPattern='offer_%'

DELETE
FROM AliasPartIndex
WHERE documentId IN (
  SELECT Id
  FROM [dbo].[Document]
  WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern
)

DELETE
FROM PersonPartIndex
WHERE documentId IN (
  SELECT Id
  FROM [dbo].[Document]
  WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern
)

DELETE
FROM TaxonomyIndex
WHERE documentId IN (
  SELECT Id
  FROM [dbo].[Document]
  WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern
)

DELETE
FROM ContainedPartIndex
WHERE documentId IN (
  SELECT Id
  FROM [dbo].[Document]
  WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern
)

DELETE
FROM ContentItemIndex
WHERE documentId IN (
  SELECT Id
  FROM [dbo].[Document]
  WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern
)

DELETE
FROM PaymentIndex
WHERE documentId IN (
  SELECT Id
  FROM [dbo].[Document]
  WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern
)

DELETE
FROM OfferIndex
WHERE documentId
IN (
  SELECT Id
  FROM [dbo].[Document]
  WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern
)

DELETE FROM AutoroutePartIndex WHERE documentId IN (
  SELECT Id
  FROM [dbo].[Document]
  WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern
)

DELETE
FROM [dbo].[Document]
WHERE JSON_VALUE(Content,'$.ContentItemId') LIKE @idPattern