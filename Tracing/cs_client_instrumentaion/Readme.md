# Weather Forecast Sample App

‏This C# based app creates an endpoint that upon access, returns random weather forecast data.

## Tracing Instrumentation

The app uses the following packages to implement tracing instrumentation in OpenTracing standard:
* Jaeger
* OpenTracing
* Opentracing.Contrib.Netcore - This is for auto tracing instrumentation.

The instrumentation is done mainly in the `Startup.cs` file.

### Triggering a Trace
To trigger a trace, you need to access the endpoint at `https://localhost:5001/weatherforecast`.
If you wish to forward the traces to Logz.io and view them in Logz.io's tracing platform. You'll need to run a collector and an agent, more info at our [shipping docs](https://docs.logz.io/shipping/) >> Tracing. 