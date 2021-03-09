CREATE TABLE [dbo].[Workers] (
    [id]            INT           IDENTITY (1, 1) NOT NULL,
    [workerName]    NVARCHAR (50) NOT NULL,
    [workerLastName]    NVARCHAR (50)  NOT NULL,
    [role]     NVARCHAR (50)  NOT NULL,
    [salary]        INT           NOT NULL,
    [status]     NVARCHAR (10)  NOT NULL
);