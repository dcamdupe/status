Intro
======

This documents the design of the API interface for the status project

Interacting with the api
======

Before calling any of the api methods, you will first need to create a session. Calling this will provide you with a session token. This session token needs to be provided with every call make to the other api methods.

Interfaces
======

* session - provides management of a session for using the API
	* POST - in: login, password, out: session_id - creates a new session, returns sessiom id
	* DELETE - in: session_id - ends a session
* status - provides interface for interacting with status data
	* POST - in: session_id, message, out: status_id - posts a new status
	* GET - in: session_id, status_id, out: status data - retrieves data for a single status
* status/search
	* GET - in: session_id, search text, pagination data, out: list of status data - searches for a list of status data for current user
* status/history
	* GET - in: session_id, pagination data, out: list of status data - retrieves list of status data for current user
* status/like - provides interface over status likes
	* POST - in: session_id, status_id - adds a like to a status