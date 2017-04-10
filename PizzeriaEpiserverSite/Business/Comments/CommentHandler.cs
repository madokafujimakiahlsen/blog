﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.Core;
using PizzeriaEpiserverSite.Models.Blocks;

namespace PizzeriaEpiserverSite.Business.Comments
{
    public class CommentHandler
    {
        private IContentRepository _contentRepository;

        public CommentHandler(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public void AddComment(ContentReference commentFolderReference, string name, string text, DateTime date)
        {
            PostedComment newCommentBlock = _contentRepository.GetDefault<PostedComment>(commentFolderReference);

            newCommentBlock.Text = text;
            newCommentBlock.Date = date;
            newCommentBlock.CommentatorName = name;
            IContent newCommentBlockInstance = newCommentBlock as IContent;
            newCommentBlockInstance.Name = name;

            _contentRepository.Save(newCommentBlockInstance, EPiServer.DataAccess.SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess); //EPiServer.Security.AccessLevel.Publish blev érror
        }

        public IEnumerable<PostedComment> LoadComments(ContentReference commentFolderReference)
        {
            if (ContentReference.IsNullOrEmpty(commentFolderReference))
            {
                return null;
            }
            return _contentRepository.GetChildren<PostedComment>(commentFolderReference);
        }
    }
}