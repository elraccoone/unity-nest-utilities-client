<div align="center">

<img src="https://raw.githubusercontent.com/elraccoone/unity-nest-utilities-client/master/.github/WIKI/logo-transparent.png" height="100px">

</br>

# Nest Utilities Client

[![openupm](https://img.shields.io/npm/v/nl.elraccoone.nest-utilities-client?label=UPM&registry_uri=https://package.openupm.com&style=for-the-badge&color=232c37)](https://openupm.com/packages/nl.elraccoone.nest-utilities-client/)
[![](https://img.shields.io/github/stars/elraccoone/unity-nest-utilities-client.svg?style=for-the-badge)]()
[![](https://img.shields.io/badge/build-passing-brightgreen.svg?style=for-the-badge)]()

Providing a set of tools allowing your application to interface with servers running rest-full API's build using the Nest Utilities package using custom services and chainable options for complete flexibility.

**&Lt;**
[**Installation**](#installation) &middot;
[**Documentation**](#documentation) &middot;
[**License**](./LICENSE.md)
**&Gt;**

</br></br>

[![npm](https://img.shields.io/badge/sponsor_the_project-donate-E12C9A.svg?style=for-the-badge)](https://paypal.me/jeffreylanters)

Hi! My name is Jeffrey Lanters, thanks for checking out my modules! I've been a Unity developer for years when in 2020 I decided to start sharing my modules by open-sourcing them. So feel free to look around. If you're using this module for production, please consider donating to support the project. When using any of the packages, please make sure to **Star** this repository and give credit to **Jeffrey Lanters** somewhere in your app or game. Also keep in mind **it it prohibited to sublicense and/or sell copies of the Software in stores such as the Unity Asset Store!** Thanks for your time.

**&Lt;**
**Made with &hearts; by Jeffrey Lanters**
**&Gt;**

</br>

</div>

# Installation

### Using the Unity Package Manager

Install the latest stable release using the Unity Package Manager by adding the following line to your `manifest.json` file located within your project's Packages directory, or by adding the Git URL to the Package Manager Window inside of Unity.

```json
"nl.elraccoone.nest-utilities-client": "git+https://github.com/elraccoone/unity-nest-utilities-client"
```

### Using OpenUPM

The module is availble on the OpenUPM package registry, you can install the latest stable release using the OpenUPM Package manager's Command Line Tool using the following command.

```sh
openupm add nl.elraccoone.nest-utilities-client
```

# Documentation

The package revolves around the CrudService. Extending this service will grant you methods to call upon the endpoints generated by nest-utilities as well as the available options provided by the chainable request builder. Each service revolves around it's respective serializable resource model.

## Getting started

Start off by creating a service for each of your resources. This is as simple as creating a new class and extending the CRUD Service provided by the Namespace. Extend the constructor with some basic parameters and your Model's Type. The Model's Type is responsible for transfering data in our resource's format.

```csharp
using ElRaccoone.NestUtilitiesClient;

public class UserService : CrudService<User> {
  public UserService () : base (
    hostname: "my-awesome-api.com",
    resource: "user"
  ) { }
}
```

To use your newly created Service, simply create an instance somewhere in your application. Preferably in some sort of Network Component. To get started with your first request, use one for the CRUD methods such as Get, Post, Delete or Patch provided by your Service. Once instanciated, a newly created Request Builder will be returned.

```csharp
public class TestComponent : MonoBehaviour {
  private UserService userService = new UserService ();

  private void Test () {
    var getUsersRequest = this.userService.Get ();
    var getUserRequest = this.userService.Get (id: "xyz");
    var createUserRequest = this.userService.Post (model: new User (...));
    var updateUserRequest = this.userService.Patch (model: new User (...));
    var deleteUserRequest = this.userService.Delete (id: "xyz");
  }
}
```

Once created your Request using the Builder, it's time to send it over to your API. To do this, yield the Send method provided by the builder. This will start the request and send over your data over to the server.

```csharp
public class TestComponent : MonoBehaviour {
  private UserService userService = new UserService ();

  private IEnumerator Test () {
    var request = this.userService.Get ();
    yield return request.Send();
  }
}
```

To confirm a request was successful, or get the response body of it, use the GetResponse method after sending your Request. When something went wrong, a request exception will be thrown containing information tp debug the problem. Use the GetRawResponse method in order to get the raw response data, this method can also throw an exception.

```csharp
public class TestComponent : MonoBehaviour {
  private UserService userService = new UserService ();

  private IEnumerator Test () {
    var request = this.userService.Get ();
    yield return request.Send();
    try {
      var users = request.GetResponse ();
    }
    catch (RequestException exception) {
      Debug.Log (exception.statusCode);
    }
  }
}
```

## Chainable Options

#### Authorize `version 1.0.0`

Sets the authorization header allow the request to authorize itself on the server.

```csharp
public RequestBuilder<ModelType> Authorize (string token);
```

#### Populate `version 1.0.0`

This parameter allows you to populate references to other collections in the response.

```csharp
public RequestBuilder<ModelType> Populate (params string[] fields);
```

#### Filter `version 1.0.0` (Obsolete)

This parameter allows you to filter the response data on one or more fields on specific values. You can filter on multiple fields by chaing the filter method.

```csharp
public RequestBuilder<ModelType> Filter (string field, string filter);
```

#### Search `version 1.0.0` (Obsolete)

This parameter allows you to search through all fields of the response using the same value. Results matching the value in at least one of the fields will be shown in the response.

```csharp
public RequestBuilder<ModelType> Search (string query);
```

This parameter allows you to search through all fields of the response using the same value. Results matching the value in at least one of the fields will be shown in the response. You can limit the fields which fields in which the algorithm searches using the search scope parameter.

```csharp
public RequestBuilder<ModelType> Search (string query, params string[] scopes);
```

#### Pick `version 1.0.0`

This parameter allows you to define which fields you want the results to contain. If one or more fields have been picked for a layer, the remaining layers will be omitted from the response. You can deep pick fields by separating fields using a dot (f.e. brewers.name).

```csharp
public RequestBuilder<ModelType> Pick (params string[] fields);
```

#### Sort `version 1.0.0`

This parameter allows you to sort the response data on one or more fields in the desired order.

```csharp
public RequestBuilder<ModelType> Sort (params string[] fields);
```

This parameter allows you to sort the response data on one or more fields in the desired order. Define descending in order to by it in descending order.

```csharp
public RequestBuilder<ModelType> Sort (string field, bool descending);
```

#### Offset `version 1.0.0`

This parameter allows you to skip the first n number of results.

```csharp
public RequestBuilder<ModelType> Offset (int amount);
```

#### Limit `version 1.0.0`

This parameter allows you to limit the response to only show the next n number of results.

```csharp
public RequestBuilder<ModelType> Limit (int amount);
```

#### Distinct `version 1.0.0`

This parameter allows you to find the distinct values for a specified field. The returned models will each contain unique values for that field. When multiple models in the actual response would have the same value, the first encountered model will be chosen based on the sort attribute.

```csharp
public RequestBuilder<ModelType> Distinct (string field);
```

#### Random `version 1.0.0`

This parameter allows you to randomize the order of the response data. This parameter holds priority over the sort parameter which means that the sort will be omitted when random is defined.

```csharp
public RequestBuilder<ModelType> Random ();
```

## Custom Service Methods

When the default CRUD service does not cover all your needs, then it is possible to extend your class with some custom methods as shown below.

```csharp
using ElRaccoone.NestUtilitiesClient;
using ElRaccoone.NestUtilitiesClient.Core;

public class UserService : CrudService<User> {
  public UserService () : base (
    hostname: "my-awesome-api.com",
    resource: "user"
  ) { }

  public RequestBuilder<User> SomethingCustom (string value) =>
    new RequestBuilder<User> (
        requestMethod: RequestMethod.POST,
        url: string.Join ("/", this.url, "somethingcustom", value),
        model: new User ());
}
```
