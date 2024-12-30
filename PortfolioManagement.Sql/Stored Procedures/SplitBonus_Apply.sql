CREATE PROCEDURE [dbo].[SplitBonus_Apply]
    @Id INT,
    @IsApply BIT
AS
BEGIN
    DECLARE 
    @ScriptID INT,
    @IsSplit BIT,
    @IsBonus BIT,
    @OldFaceValue FLOAT = NULL,  
    @NewFaceValue FLOAT = NULL,  
    @FromRatio INT = NULL,
    @ToRatio INT = NULL

    SELECT      @ScriptID     = SB.ScriptID,
                @IsSplit      = SB.IsSplit,
                @IsBonus      = SB.IsBonus,
                @OldFaceValue = SB.OldFaceValue,
                @NewFaceValue = SB.NewFaceValue,
                @FromRatio    = SB.FromRatio,
                @ToRatio      = SB.ToRatio
    FROM        SplitBonus SB
    WHERE       SB.Id = @Id
        
    BEGIN TRANSACTION BulkSplitBonusUpdate 
	BEGIN TRY
        -- Update IsApply only if the procedure is to be applied
        IF @IsApply = 1
        BEGIN
            UPDATE SplitBonus
            SET IsApply = 1
            WHERE Id = @Id;
        END

        IF @IsSplit = 1
        BEGIN
		    UPDATE	dbo.[StockTransaction]
		    SET		[ScriptId]				= @ScriptId		,	
				    [Qty]					= [Qty] * @OldFaceValue / @NewFaceValue,	
				    [Price]					= [Price] * @NewFaceValue / @OldFaceValue	
		    WHERE	ScriptId				= @ScriptID

            UPDATE	dbo.[ScriptPrice]
		    SET	    [Price]					= [Price] * @NewFaceValue / @OldFaceValue,
                    [Volume]                = [Volume] * @OldFaceValue / @NewFaceValue
		    WHERE	ScriptId				= @ScriptID

            UPDATE	        T
		    SET	            [Qty]           = T.[Qty] * @OldFaceValue / @NewFaceValue
            FROM            dbo.[TransactionProtfolio] T
                INNER JOIN  dbo.[StockTransaction] ST
                    ON      T.TransactionId = ST.Id
		    WHERE	ST.ScriptId		        = @ScriptID

	        UPDATE	dbo.[Portfolio]
		    SET	    [BuyQty]				= [BuyQty] * @OldFaceValue / @NewFaceValue,
                    [BuyPrice]              = [BuyPrice] * @NewFaceValue / @OldFaceValue,
					[SellQty]				= [SellQty] * @OldFaceValue / @NewFaceValue,
                    [SellPrice]             = [SellPrice] * @NewFaceValue / @OldFaceValue
		    WHERE	ScriptId				= @ScriptID

            UPDATE	dbo.[ScriptDaySummary]
		    SET	    [Price]					= [Price] * @NewFaceValue / @OldFaceValue,
                    [Volume]				= [Volume] * @OldFaceValue / @NewFaceValue,
                    [PreviousDay]           = [PreviousDay] * @NewFaceValue / @OldFaceValue,
                    [Open]                  = [Open] * @NewFaceValue / @OldFaceValue,
                    [Close]                 = [Close] * @NewFaceValue / @OldFaceValue,
                    [High]                  = [High] * @NewFaceValue / @OldFaceValue,
                    [Low]                   = [Low] * @NewFaceValue / @OldFaceValue,
                    [High52Week]            = [High52Week] * @NewFaceValue / @OldFaceValue,
                    [Low52Week]             = [Low52Week] * @NewFaceValue / @OldFaceValue
		    WHERE	ScriptId				= @ScriptID
        END

        IF @IsBonus = 1
        BEGIN
			SET		@ToRatio = @ToRatio + @FromRatio

		    UPDATE	dbo.[StockTransaction]
		    SET		[Qty]					= [Qty] * @ToRatio / @FromRatio,	
				    [Price]					= [Price] * @FromRatio / @ToRatio	
		    WHERE	ScriptId				= @ScriptID

            UPDATE	dbo.[ScriptPrice]
		    SET	    [Price]					= [Price] * @FromRatio / @ToRatio,
                    [Volume]                = [Volume] * @ToRatio / @FromRatio
		    WHERE	ScriptId				= @ScriptID

            UPDATE	        T
		    SET	            [Qty]           = T.[Qty] * @ToRatio / @FromRatio
            FROM            dbo.[TransactionProtfolio] T
                INNER JOIN  dbo.[StockTransaction] ST
                    ON      T.TransactionId = ST.Id
		    WHERE	ST.ScriptId		        = @ScriptID

            UPDATE	dbo.[Portfolio]
		    SET	    [BuyQty]				= [BuyQty] * @ToRatio / @FromRatio,
                    [BuyPrice]              = [BuyPrice] * @FromRatio / @ToRatio,
					[SellQty]				= [SellQty] * @ToRatio / @FromRatio,
                    [SellPrice]             = [SellPrice] * @FromRatio / @ToRatio
		    WHERE	ScriptId				= @ScriptID

            UPDATE	dbo.[ScriptDaySummary]
		    SET	    [Price]					= [Price] * @FromRatio / @ToRatio,
                    [Volume]				= [Volume] * @ToRatio / @FromRatio,
                    [PreviousDay]           = [PreviousDay] * @ToRatio / @FromRatio,
                    [Open]                  = [Open] * @ToRatio / @FromRatio,
                    [Close]                 = [Close] * @ToRatio / @FromRatio,
                    [High]                  = [High] * @ToRatio / @FromRatio,
                    [Low]                   = [Low] * @ToRatio / @FromRatio,
                    [High52Week]            = [High52Week] * @ToRatio / @FromRatio,
                    [Low52Week]             = [Low52Week] * @ToRatio / @FromRatio
		    WHERE	ScriptId				= @ScriptID
        END

        IF @@TRANCOUNT > 0
			COMMIT TRANSACTION BulkSplitBonusUpdate

    END TRY
	BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION BulkSplitBonusUpdate
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY();
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
	END CATCH

END