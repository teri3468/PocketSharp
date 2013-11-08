﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketSharp.Models;
using System.Threading;

namespace PocketSharp
{
  public interface IPocketClient
  {
    #region properties
    /// <summary>
    /// callback URLi for API calls
    /// </summary>
    /// <value>
    /// The callback URI.
    /// </value>
    string CallbackUri { get; set; }

    /// <summary>
    /// Accessor for the Pocket API key
    /// see: http://getpocket.com/developer
    /// </summary>
    /// <value>
    /// The consumer key.
    /// </value>
    string ConsumerKey { get; set; }

    /// <summary>
    /// Code retrieved on authentification
    /// </summary>
    /// <value>
    /// The request code.
    /// </value>
    string RequestCode { get; set; }

    /// <summary>
    /// Code retrieved on authentification-success
    /// </summary>
    /// <value>
    /// The access code.
    /// </value>
    string AccessCode { get; set; }
    #endregion

    #region account methods
    /// <summary>
    /// Retrieves the requestCode from Pocket, which is used to generate the Authentication URI to authenticate the user
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.NullReferenceException">Authentication methods need a callbackUri on initialization of the PocketClient class</exception>
    /// <exception cref="PocketException"></exception>
    Task<string> GetRequestCode();

    /// <summary>
    /// Retrieves the requestCode from Pocket, which is used to generate the Authentication URI to authenticate the user
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.NullReferenceException">Authentication methods need a callbackUri on initialization of the PocketClient class</exception>
    /// <exception cref="PocketException"></exception>
    Task<string> GetRequestCode(CancellationToken cancellationToken);

    /// <summary>
    /// Generate Authentication URI from requestCode
    /// </summary>
    /// <param name="requestCode">The requestCode. If no requestCode is supplied, the property from the PocketClient intialization is used.</param>
    /// <returns>
    /// A valid URI to redirect the user to.
    /// </returns>
    /// <exception cref="System.NullReferenceException">Call GetRequestCode() first to receive a request_code</exception>
    Uri GenerateAuthenticationUri(string requestCode = null);

    /// <summary>
    /// Requests the access code and username after authentication
    /// The access code has to permanently be stored within the users session, and should be passed in the constructor for all future PocketClient initializations.
    /// </summary>
    /// <param name="requestCode">The request code.</param>
    /// <returns>
    /// The authenticated user
    /// </returns>
    /// <exception cref="System.NullReferenceException">Call GetRequestCode() first to receive a request_code</exception>
    Task<PocketUser> GetUser(string requestCode = null);

    /// <summary>
    /// Requests the access code and username after authentication
    /// The access code has to permanently be stored within the users session, and should be passed in the constructor for all future PocketClient initializations.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="requestCode">The request code.</param>
    /// <returns>
    /// The authenticated user
    /// </returns>
    /// <exception cref="System.NullReferenceException">Call GetRequestCode() first to receive a request_code</exception>
    Task<PocketUser> GetUser(CancellationToken cancellationToken, string requestCode = null);

    /// <summary>
    /// Registers a new account.
    /// Account has to be activated via a activation email sent by Pocket.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">All parameters are required</exception>
    /// <exception cref="System.FormatException">
    /// Invalid email address.
    /// or
    /// Invalid username. Please only use letters, numbers, and/or dashes and between 1-20 characters.
    /// or
    /// Invalid password.
    /// </exception>
    /// <exception cref="PocketException"></exception>
    Task<bool> RegisterAccount(string username, string email, string password);

    /// <summary>
    /// Registers a new account.
    /// Account has to be activated via a activation email sent by Pocket.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="username">The username.</param>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">All parameters are required</exception>
    /// <exception cref="System.FormatException">Invalid email address.
    /// or
    /// Invalid username. Please only use letters, numbers, and/or dashes and between 1-20 characters.
    /// or
    /// Invalid password.</exception>
    /// <exception cref="PocketException"></exception>
    Task<bool> RegisterAccount(CancellationToken cancellationToken, string username, string email, string password);
    #endregion

    #region add methods
    /// <summary>
    /// Adds a new item to pocket
    /// </summary>
    /// <param name="uri">The URL of the item you want to save</param>
    /// <param name="tags">A comma-separated list of tags to apply to the item</param>
    /// <param name="title">This can be included for cases where an item does not have a title, which is typical for image or PDF URLs. If Pocket detects a title from the content of the page, this parameter will be ignored.</param>
    /// <param name="tweetID">If you are adding Pocket support to a Twitter client, please send along a reference to the tweet status id. This allows Pocket to show the original tweet alongside the article.</param>
    /// <returns>
    /// A simple representation of the saved item which doesn't contain all data (is only returned by calling the Retrieve method)
    /// </returns>
    /// <exception cref="System.FormatException">(1) Uri should be absolute.</exception>
    /// <exception cref="PocketException"></exception>
    Task<PocketItem> Add(Uri uri, string[] tags = null, string title = null, string tweetID = null);

    /// <summary>
    /// Adds a new item to pocket
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="uri">The URL of the item you want to save</param>
    /// <param name="tags">A comma-separated list of tags to apply to the item</param>
    /// <param name="title">This can be included for cases where an item does not have a title, which is typical for image or PDF URLs. If Pocket detects a title from the content of the page, this parameter will be ignored.</param>
    /// <param name="tweetID">If you are adding Pocket support to a Twitter client, please send along a reference to the tweet status id. This allows Pocket to show the original tweet alongside the article.</param>
    /// <returns>
    /// A simple representation of the saved item which doesn't contain all data (is only returned by calling the Retrieve method)
    /// </returns>
    /// <exception cref="System.FormatException">(1) Uri should be absolute.</exception>
    /// <exception cref="PocketException"></exception>
    Task<PocketItem> Add(CancellationToken cancellationToken, Uri uri, string[] tags = null, string title = null, string tweetID = null);
    #endregion

    #region get methods
    /// <summary>
    /// Retrieves items from pocket
    /// with the given filters
    /// </summary>
    /// <param name="state">The state.</param>
    /// <param name="favorite">The favorite.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="sort">The sort.</param>
    /// <param name="search">The search.</param>
    /// <param name="domain">The domain.</param>
    /// <param name="since">The since.</param>
    /// <param name="count">The count.</param>
    /// <param name="offset">The offset.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketItem>> Get(
      State? state = null,
      bool? favorite = null,
      string tag = null,
      ContentType? contentType = null,
      Sort? sort = null,
      string search = null,
      string domain = null,
      DateTime? since = null,
      int? count = null,
      int? offset = null
    );

    /// <summary>
    /// Retrieves items from pocket
    /// with the given filters
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="state">The state.</param>
    /// <param name="favorite">The favorite.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="sort">The sort.</param>
    /// <param name="search">The search.</param>
    /// <param name="domain">The domain.</param>
    /// <param name="since">The since.</param>
    /// <param name="count">The count.</param>
    /// <param name="offset">The offset.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketItem>> Get(
      CancellationToken cancellationToken,
      State? state = null,
      bool? favorite = null,
      string tag = null,
      ContentType? contentType = null,
      Sort? sort = null,
      string search = null,
      string domain = null,
      DateTime? since = null,
      int? count = null,
      int? offset = null
    );

    /// <summary>
    /// Retrieves an item by a given ID
    /// Note: The Pocket API contains no method, which allows to retrieve a single item, so all items are retrieved and filtered locally by the ID.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<PocketItem> Get(int itemID);

    /// <summary>
    /// Retrieves an item by a given ID
    /// Note: The Pocket API contains no method, which allows to retrieve a single item, so all items are retrieved and filtered locally by the ID.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<PocketItem> Get(CancellationToken cancellationToken, int itemID);

    /// <summary>
    /// Retrieves all items by a given filter
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketItem>> Get(RetrieveFilter filter);

    /// <summary>
    /// Retrieves all items by a given filter
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketItem>> Get(CancellationToken cancellationToken, RetrieveFilter filter);

    /// <summary>
    /// Retrieves all available tags.
    /// Note: The Pocket API contains no method, which allows to retrieve all tags, so all items are retrieved and the associated tags extracted.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketTag>> GetTags();

    /// <summary>
    /// Retrieves all available tags.
    /// Note: The Pocket API contains no method, which allows to retrieve all tags, so all items are retrieved and the associated tags extracted.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketTag>> GetTags(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves items by tag
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketItem>> SearchByTag(string tag);

    /// <summary>
    /// Retrieves items by tag
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketItem>> SearchByTag(CancellationToken cancellationToken, string tag);

    /// <summary>
    /// Retrieves items which match the specified search string in title and URI
    /// </summary>
    /// <param name="searchString">The search string.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Search string length has to be a minimum of 2 chars</exception>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketItem>> Search(string searchString, bool searchInUri = true);

    /// <summary>
    /// Retrieves items which match the specified search string in title and URI
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="searchString">The search string.</param>
    /// <param name="searchInUri">if set to <c>true</c> [search in URI].</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Search string length has to be a minimum of 2 chars</exception>
    /// <exception cref="PocketException"></exception>
    Task<List<PocketItem>> Search(CancellationToken cancellationToken, string searchString, bool searchInUri = true);

    /// <summary>
    /// Finds the specified search string in title and URI for an available list of items
    /// </summary>
    /// <param name="availableItems">The available items.</param>
    /// <param name="searchString">The search string.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Search string length has to be a minimum of 2 chars</exception>
    /// <exception cref="PocketException"></exception>
    List<PocketItem> Search(List<PocketItem> availableItems, string searchString);
    #endregion

    #region modify methods
    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Archive(int itemID);

    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Archive(PocketItem item);

    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Archive(CancellationToken cancellationToken, int itemID);

    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Archive(CancellationToken cancellationToken, PocketItem item);

    /// <summary>
    /// Un-archives the specified item (alias for Readd).
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Unarchive(int itemID);

    /// <summary>
    /// Unarchives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Unarchive(PocketItem item);

    /// <summary>
    /// Un-archives the specified item (alias for Readd).
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Unarchive(CancellationToken cancellationToken, int itemID);

    /// <summary>
    /// Unarchives the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Unarchive(CancellationToken cancellationToken, PocketItem item);

    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Favorite(int itemID);

    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Favorite(PocketItem item);

    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Favorite(CancellationToken cancellationToken, int itemID);

    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Favorite(CancellationToken cancellationToken, PocketItem item);

    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Unfavorite(int itemID);

    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Unfavorite(PocketItem item);

    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Unfavorite(CancellationToken cancellationToken, int itemID);

    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Unfavorite(CancellationToken cancellationToken, PocketItem item);

    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Delete(int itemID);

    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    Task<bool> Delete(PocketItem item);

    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> Delete(CancellationToken cancellationToken, int itemID);

    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    Task<bool> Delete(CancellationToken cancellationToken, PocketItem item);
    #endregion

    #region modify tags methods
    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> AddTags(int itemID, string[] tags);

    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> AddTags(PocketItem item, string[] tags);

    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> AddTags(CancellationToken cancellationToken, int itemID, string[] tags);

    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> AddTags(CancellationToken cancellationToken, PocketItem item, string[] tags);

    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTags(int itemID, string[] tags);

    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTags(PocketItem item, string[] tags);

    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTags(CancellationToken cancellationToken, int itemID, string[] tags);

    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTags(CancellationToken cancellationToken, PocketItem item, string[] tags);

    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTag(int itemID, string tag);

    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTag(PocketItem item, string tag);

    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTag(CancellationToken cancellationToken, int itemID, string tag);

    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTag(CancellationToken cancellationToken, PocketItem item, string tag);

    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTags(int itemID);

    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTags(PocketItem item);

    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTags(CancellationToken cancellationToken, int itemID);

    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RemoveTags(CancellationToken cancellationToken, PocketItem item);

    /// <summary>
    /// Replaces all existing tags with the given tags in an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> ReplaceTags(int itemID, string[] tags);

    /// <summary>
    /// Replaces all existing tags with the given new ones in an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> ReplaceTags(PocketItem item, string[] tags);

    /// <summary>
    /// Replaces all existing tags with the given tags in an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> ReplaceTags(CancellationToken cancellationToken, int itemID, string[] tags);

    /// <summary>
    /// Replaces all existing tags with the given new ones in an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> ReplaceTags(CancellationToken cancellationToken, PocketItem item, string[] tags);

    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RenameTag(int itemID, string oldTag, string newTag);

    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RenameTag(PocketItem item, string oldTag, string newTag);

    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RenameTag(CancellationToken cancellationToken, int itemID, string oldTag, string newTag);

    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<bool> RenameTag(CancellationToken cancellationToken, PocketItem item, string oldTag, string newTag);
    #endregion

    #region statistics methods
    /// <summary>
    /// Statistics from the user account.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<PocketStatistics> GetUserStatistics();

    /// <summary>
    /// Statistics from the user account.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<PocketStatistics> GetUserStatistics(CancellationToken cancellationToken);

    /// <summary>
    /// Returns API usage statistics.
    /// If a request was made before, the data is returned synchronously from the cache.
    /// Note: This method only works for authenticated users with a given AccessCode.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<PocketLimits> GetUsageLimits();

    /// <summary>
    /// Returns API usage statistics.
    /// If a request was made before, the data is returned synchronously from the cache.
    /// Note: This method only works for authenticated users with a given AccessCode.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    Task<PocketLimits> GetUsageLimits(CancellationToken cancellationToken);
    #endregion
  }
}