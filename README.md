# SetHttpHeaders
Biztalk pipeline component to build a string with http headers for the WCF-webhttp adapter.
This component allows you to set httpheaders with values from your promoted properties. It is posible to set one or more headers by separating the property paths and header names by semi colon.  

## Properties
| Property         | Type   | Description                                                                                                                  |
|------------------|--------|------------------------------------------------------------------------------------------------------------------------------|
| Disabled         | bool   | Disables the component. Default value: false                                                                                 |
| Promote Property | bool   | Specifies whether the property should be promoted or just written to the context. Default value: false                       |
| Property Paths   | string | Paths to properties separated by semi colon, i.e. http//:examplenamespace1#Myproperty1;http//:examplenamespace2#Myproperty2  |
| Destination Path | string | The property Path to where the returned value will be promoted to, i.e. http//:examplenamespace#Myproperty                   |
| Header Names     | string | Name of each http header seperated by semi colon, i.e. MyHeader1;MyHeader2                                                   |
