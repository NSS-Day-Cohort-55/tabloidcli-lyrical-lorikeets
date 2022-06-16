SELECT n.id,
                                               n.Title AS NoteTitle,
                                               n.Context AS NoteContext,
                                               n.CreateDateTime as NoteCreateDateTime,
                                               p.PostId as NotePostId,
                                          FROM Note n 
                                               LEFT JOIN Post a on p.PostId = p.Id                                              
                                         WHERE p.PostId = @postId