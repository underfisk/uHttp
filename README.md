# uHttp
This http librarys are designed mostly for Unity c#, which will let you make synchronized requests and extending the original unity WWW with optional asynchronous method 

### Description ###
By using uHttp you have some good features such as easy Post methods by using my custom HttpForm class which allows you to create an instance of HttpForm object and add fields. This feature is basically a extend of Unity but it's more designed for raw data such as Login data, Update data and not for multiple purposes like File Upload.

> This package comes with some Unity scripts and an extended version for Editor which allows you to test the post method on the first given URL.

### Example of usage ###
- Create a new instance of HttpRequest 
`var request = new HttpRequest();`
- Create a new instance of HttpForm if you wish to use Post Method
`var formData = new HttpForm();`
- To add a text field or another datatype acceptable on Post, you'll have to create a new field and send the value (Acceptable values : String & Integer) for now but if you want to implement more it's pretty simple
`formData.AddField("field_name","value"); `
`formData.AddField("field_name",123);`
- Now to send the request you simply need to call Post function
`request.Post("URL_Here",formData);`
- To check if the request was succesfully or not you'll have some attributes of request Object which you are able to access. This is an practical example:
> Verify is `request.isDone` by getting the attr -> request.isDone which returns the request state after in a nested if you verify if the request is not an error or an web exception by using `!request.isError && request.statusCode == HttpStatusCode.OK` and finally to show the content body you just need to simply access `request.ContentResponse`.
- (Optional) If you want to verify wheter the request is Json you just have to access another attribute which returns a bool state. `request.isJson`
- To make a Get Request is the same way of Post except the formData you'll have have to `request.Get(url)` but this function is not much finished because my main focus was Post but soon i'll be adding Put and also finish Get but the main purpose of this package is to help newbies

> Disclamer: Im not responsible for any act you do after you take this scripts, they are licensed under MIT License
