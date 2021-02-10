# Weather Forecast Sample App

‏This C# based app creates an endpoint that upon access, returns random weather forecast data.

## Tracing Instrumentation

The app uses the following packages to implement tracing instrumentation in OpenTracing standard:
* Jaeger
* OpenTracing
* Opentracing.Contrib.Netcore - This is for auto tracing instrumentation.

The instrumentation is done mainly in the `Startup.cs` file.

