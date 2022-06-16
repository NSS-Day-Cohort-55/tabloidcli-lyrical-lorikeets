﻿ALTER TABLE PostTag
   DROP CONSTRAINT FK_PostTag_Tag;

ALTER TABLE PostTag
    ADD CONSTRAINT FK_PostTag_Tag
    FOREIGN KEY (TagId)
    REFERENCES Tag (Id)
    ON DELETE CASCADE;