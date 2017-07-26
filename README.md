# School tests - C# semestral project
This project is for teachers and students - gives teachers ability to create tests and see tests results and students to write these tests. 
If you want to improve it, feel free to send pull request.

## Technologies
- .NET 4.6.1
- Windows Forms with [MaterialSkin](https://github.com/IgnaceMaes/MaterialSkin) theme
- MySQL Database in background - in C# using [Shaolinq](https://github.com/tumtumtum/Shaolinq) library
- Solution + Project is for VisualStudio 2017

other libraries used:
- [ValueTupple](https://www.nuget.org/packages/System.ValueTuple/)
- [Json.Net](http://www.newtonsoft.com/json) - for serializing questions data
- [BCrypt](https://www.nuget.org/packages/BCrypt/1.0.0) - for storing teachers credentials inside db

## Structure
App follows MVC pattern, so you can find all models, controllers and views in corresponding folders.
In the whole app there's available appContext, which contains these control structures:
- Router : handles what controller has now the controll and gives them power. If you want to call any controller/action you must use Router
- ViewManager : handles all views - and calls proper view to render. Also handles displaying error and info messages + form heading if there's logged student.
- DB : main Shaolinq database object - through this you manipulate with model.
- Session : key-value chain to store infos you need to be available in the whole app (like current user)

All the controllers must be inside Controllers folder, inherit from BaseController and so implement method ProcessAction() - which handles how to respond to caller's action. Thank's to that you can access appContext.

All the views must be inside Views folder, inherit and implement BaseView class. All the magic happends inside Render() function, where you can access formToRender variable - which is form, you can render to. 
There are two types of Views 
- controller views:  
   + corresponds to concrete controller+action
   + called by ViewManager.Render
   + inside Views/<controller_name>/<action>.cs and corresponding namespaces
- partial views:   
   + shared parts for controller views (for example top menu)
   + called by ViewManager.RenderPartial
   + inside Views/Partials/<partial_name>.

Main db setup is inside Models/DBModel.cs 


## Notes
+ Start of app and it's initialization is inside Form1.cs. 
+ Database configuration is inside DBModel.
+ Default teacher's login & password can be setted inside SetUpController.
+ When you pass to view's Render in data keyval 
   - "errors" : "some message" there will be displayed on the top the "some message" in red
   - "infos" : "some message"    ^ in blue 

## Translations
App is translation-ready, there's Translation resource, so you can fill other languages than Slovak and Czech if you like.  
What language is used is defined inside Form1.cs.

## Adding more question types
Each Question type must have it's correctly-named controller in Controllers/QuestionTypes. It must handle two actions - Process and SaveAnswer.  
Process action is called when teacher saves the created/edited test, so you have inside parameters variable questionTab which represent's current TabPage, you were rendering your view to (Edit/New). If tabPage has something in Tag property, than it is elready existing question and inside it is its id.  
SaveAnswer is called whenever student submits test. Inside parameters you have answerTab which is TabPage with answer you were rendering to ( Show ) and question which is QuestionModel of answered question.  
Each question type must also have it's folder inside Views/Questions folder. Inside that folder there must be Views for these actions:
- Edit - used when teacher is editing question
- New - used when teacher clicks "New Question"
- Show - used when the student displays question
- Result - used when student sees this question in results
- TeacherResult - used when teacher sees this question in results
QuestionType model should be in Models/QuestionTypes/<your_type>

To be sure, just check how is implemented already existing question types.

## Known issues
- Shaolinq - at least in MySQL saves all the strings as varchar(512) so all the question serialized data must pass into this limit.

## Todo's
- Test editor - ability to remove question - watch out the order in tab text!
- Teachers part - ability to add/remove teachers, edit passwords


