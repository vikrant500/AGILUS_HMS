The disease code includes several new additions to the standard blog code. Please note that these additions are based on the original blog code, not the version updated by Rishika.

Here are the new files created (it is recommended to copy and paste them directly into the latest blog code):

1. In the `api` directory, within the `models` folder, we added a new `Disease.js` model and updated the `index.js` file.

2. In the `client` directory, the following changes were made:

	- Updated `App.js` (added new routes)
	- Added `Disease.js`

3. In the `pages` folder of the `client` directory, the following changes were made:

	- Created `AboutDisease.js` and `CreateDisease.js`
	- Updated `CreatePost.js` (it now alerts whenever a post is made successfully)

Make sure to use the project mongo key in api/index.js to access. No need to create a table in mongoDB as well as it will be created automatically.

Ensure that all the above-mentioned changes are added to or updated in the existing blog code that Rishika updated, as she made some bug fixes in that code as well.

The code I am uploading to GitHub is a working model of both the disease and the posts. However, please make sure to add or update using this code and not use this code directly.
