# Weather Forecast Sample App

‏This C# based app creates an endpoint that upon access, returns random weather forecast data.

## Tracing Instrumentation

The app uses the following packages to implement tracing instrumentation in OpenTracing standard:
* Jaeger
* OpenTracing
* Opentracing.Contrib.Netcore - This is for auto tracing instrumentation.

The instrumentation is done mainly in the `Program.cs` file, the we call the same tracer in `Statup.cs`.

### Triggering a Trace
When you run the program, it will create a trace for the initialization process, and a trace from calling the API.
To trigger a trace, you need to access the endpoint at `https://localhost:5001/weatherforecast` (it will be called once automatically).
If you wish to forward the traces to Logz.io and view them in Logz.io's tracing platform. You'll need to run a collector and an agent, more info at our [shipping docs](https://docs.logz.io/shipping/) >> Tracing. 