# Malicious
#### A C# library that detects whether or not a user input string is malicious

## Installation

1. Execute `git submodule add http://github.com/Datasilk/Malicious` to add this library to your git project

That's it! Now let's check a user input string to see if it is malicious.

```
using Utility.Malicious;

public class WebService {
	public bool UpdateTitle(string title){
		//check if user input is malicious
		if(Malicious.isMalicious(title, Malicious.InputType.Text)){
			return false;
		}
		//update title with user input...
	}
}
```

## Options

| Enumerator | Definition  
--- | ---
|`TextOnly`|The string is checked for HTML, Javascript, SQL injection, and other forms of command injection
|`ContainsJavaScript`| Checks if string is valid JavaScript. Also, checks for malicious Javascript code, such as iframe injection, DOM manipulation, and XSS attacks. 
|`ContainsHtml`| If `true`, checks for malicious HTML code, such as iframes, embed tags, and script tags. If `false`, checks for any HTML tags.
|`IsJson`|If `true`, checks string for proper JSON syntax

---

### What is User Input?
When dealing with application development, user input consists of any string of data that is sent to the application from the user. When accessing a web page, the user sends a URL to a web server that parses the URL and sends the user content based on the URL string. Any form fields sent to the web server in a form `POST` is another example.

---

## Make Less Mistakes In Your Code

#### 1. Don't include user input directly in generated SQL queries. 

**Don't**
```
var cmd = new SqlCommand();
cmd.CommandText = "SELECT * FROM Users WHERE userId=" + Request.Query["id"];
```

---

Instead, use stored procedures and parameterized queries.

**Do**
```
var id = 0;
int.TryParse(Request.Query["id"], out id);
if(id > 0){
	//id is valid
	var cmd = new SqlCommand();
	cmd.CommandText = "EXEC User_Details @userId=@userId";
	cmd.Parameters.Add(new SqlParameter("@userId", id));
	var reader = cmd.ExecuteReader();
}
```

#### 2. Don't include user input directly in the folder structure while accessing files in code

**Don't**
```
var text = File.ReadAllText('/Content/user/' + userId + '/' + Request.Query["id"] + '.json');
```

---

**Do**
```
var id = 0;
int.TryParse(Request.Query["id"], out id);
if(id > 0){
	//user input is infact an integer
	var text = File.ReadAllText('/Content/user/' + userId + '/' + id + '.json');
}
```

#### 3. Don't include user input in Reflections

**Don't**
```
var paths = Request.Path.ToString().Split("/");
Type type = Type.GetType("MyProject.Services." + paths[0]);
MethodInfo method = type.GetMethod(paths[1]);
```

**Do**
```
var paths = Request.Path.ToString().Split("/");
var className = "";
switch(paths[0]){
	case "User": case "Projects": case "Home": case "Dashboard":
	className = paths[0]; 
	break;
}
if(className != "") {
	Type type = Type.GetType("MyProject.Services." + paths[0]);
	MethodInfo method = type.GetMethod(paths[1]);
}
```