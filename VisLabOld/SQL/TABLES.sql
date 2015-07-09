CREATE TABLE Files
(
         [ID] int IDENTITY(1,1) NOT NULL UNIQUE,
         [FileName] nvarchar(255) NOT NULL,
         [Extension] nchar(3) NOT NULL,
         [Content] varbinary(MAX) NOT NULL
)
GO
---------------------------------------------------------------------------
CREATE PROCEDURE GetDocumentSummary 
(
    @DocumentID int,
    @DocumentSummary nvarchar(MAX) OUTPUT
)
AS
SET NOCOUNT ON
SELECT  @DocumentSummary=Convert(nvarchar(MAX), DocumentSummary)
FROM    Production.Document
WHERE   DocumentID=@DocumentID
GO
---------------------------------------------------------------------------
INSERT INTO Files
SELECT 'D:\MOPO3OB\Desktop\BW\Olaine (Dymanic)\Olaine.fzi', 'FZI', BulkColumn
FROM OPENROWSET 
    (BULK 'D:\MOPO3OB\Desktop\BW\Olaine (Dymanic)\Olaine.fzi', SINGLE_BLOB) rowset
UNION
SELECT 'D:\MOPO3OB\Desktop\BW\Olaine (Dymanic)\Olaine.fma', 'FMA', BulkColumn
FROM OPENROWSET 
    (BULK 'D:\MOPO3OB\Desktop\BW\Olaine (Dymanic)\Olaine.fma', SINGLE_BLOB) rowset   
GO
---------------------------------------------------------------------------
-- 'D:\MOPO3OB\Desktop\BW\Olaine (Dymanic)\Olaine.fzi'
SELECT CAST(content AS varchar(max)), * FROM Files
DELETE FROM Files