# Running the application locally:
1. Install Free download Visual Studio from the following link:
https://visualstudio.microsoft.com/downloads/
2. Clone the repository.
3. From the cloned repository folder, open the DiscussionAPI_Application.sln in Visual Studio.
4. Open Visual studio->View->Server Explorer.
5. In the Server Explorer pane, right click on Data Connections->Add Connection.
6. Select the Data Source as Microsoft SQL Server Database File (SqlClient)
7. Browse the DatabaseDiscussion.mdf from the cloned folder inside DiscussionAPI_Application\DatabaseConnection
8. Run the existing application by clicking green arrow (IIS Express) on top ![image](https://user-images.githubusercontent.com/72108347/170920233-a00b6a2d-f98d-4e8c-9a2b-e64eed488f61.png)
 or F5 key.
 8. After running the application, the browser will open with the following url https://localhost:44365/ (could be different at your end).
 9.Use any API platform to test the APIs. (I used Postman)


# API Specifications:
## 1. Create a new discussion:
This endpoint starts a new discussion by any user.
  Endpoint: 
  ```
  https://localhost:44365/api/discussions (POST)
  ```
  Request body:
  ```
    {
    "Question":"some question", // Required String parameter
    "userId" :"any user id" // Required String parameter
    }
  ```
  Response:
  ```
  "Added successfully!"
  ```

  
 ## 2. Create a new response to a discussion or a comment:
 This endpoint allows any user to respond to a discussion or a comment.
  Endpoint: 
  ```
  https://localhost:44365/api/replies (POST)
  ```

    Request body:
  ```
    {
    "Content":"Response content", // string parameter
    "userId" :"any user id",  //string parameter
    "discussionId": Id created in discussion table to which you are responding, //Integer parameter
    "parentId": Id created in Discussions table/ Replies table to which you are responding, //Integer parameter
    "parentType":"Discussion" OR "Reply" (Based on whether you are responding to a reply or a discussion) // String Parameter
    }
  ```
    
   Response:
  ```
   "Added successfully!"
  ```
  
  ## 3. Retrieve all replies to a given discussion:
   Any user can retrieve all the comments available in the database in a flat tree for given discussion
   Endpoint: 
   ```
   https://localhost:44365/api/discussions/id/replies (GET)
   ```
  Response:
  ```
   [
    {
        "Id": 8,
        "Content": "I want to learn Maths!",
        "User_Id": "stud_123",
        "Discussion_Id": 2,
        "Parent_id": 2,
        "Parent_type": "Discussion"
    },
    {
        "Id": 9,
        "Content": "I love Maths. I would like that too!",
        "User_Id": "stud_234",
        "Discussion_Id": 2,
        "Parent_id": 8,
        "Parent_type": "Reply     "
    },
    {
        "Id": 1011,
        "Content": "I love Science. I want to learn that!",
        "User_Id": "stud_345",
        "Discussion_Id": 2,
        "Parent_id": 8,
        "Parent_type": "Reply     "
    },
    {
        "Id": 1014,
        "Content": "I love Science. I want to learn that!",
        "User_Id": "stud_345",
        "Discussion_Id": 2,
        "Parent_id": 8,
        "Parent_type": "reply     "
    }
]
  ```

# Relevant files
## 1. [XUnit Unit Testing files]([https://github.com/ApurvaB17/DiscussionAPI_Application/tree/master/DiscussionsAPI_UnitTest]
      ```
      DiscussionsControllerTest.cs : To have the Discussions API endpoint tests.
      ```
      ```
      RepliesControllerTest.cs : To have the Replies API endpoint tests.
      ```

## 2. [Controllers]([https://github.com/ApurvaB17/DiscussionAPI_Application/tree/master/DiscussionAPI_Application/Controllers]
      For all the API methods related to Discussions , refer to the 
      ```
      DiscussionsController.cs
      ```
      1. [HttpGet] Get() : To retrieve all the discussions.
      2. [HttpGet] Get(int id) : To retrieve a discussion based on an id.
      3. [HttpGet] GetReplies(int id) : To retrieve all the replies of a specific discussion.
      4. [HttpPost] Post(Discussions discussion) : To create a new discussion.
      
        For all the API methods related to Replies , refer to the 
      ```
      RepliesController.cs
      ```
      1. [HttpGet] Get() : To retrieve all the replies.
      2. [HttpGet] Get(int id) : To retrieve a reply based on an id.
      4. [HttpPost] Post(Discussions discussion) : To create a new reply.

## 3. [Models](https://github.com/ApurvaB17/DiscussionAPI_Application/tree/master/DiscussionAPI_Application/Models)
      Class structures mapped to tables are created in 
      ```
      Discussions.cs : For Discussion table.
      ```
      ```
      Replies.cs: For Replies table.
      ```
