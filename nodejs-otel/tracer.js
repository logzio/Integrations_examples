'use strict';

const opentelemetry = require('@opentelemetry/api');
const { registerInstrumentations } = require('@opentelemetry/instrumentation');
const { BasicTracerProvider, ConsoleSpanExporter, SimpleSpanProcessor } = require('@opentelemetry/tracing');
const { CollectorTraceExporter } = require('@opentelemetry/exporter-collector');
const { Resource } = require('@opentelemetry/resources');
const { ResourceAttributes } = require('@opentelemetry/semantic-conventions');
const { getNodeAutoInstrumentations } = require('@opentelemetry/auto-instrumentations-node');

// const { CollectorTraceExporter } = require('@opentelemetry/exporter-collector-grpc');
// const { CollectorTraceExporter } = require('@opentelemetry/exporter-collector-proto');

// opentelemetry.diag.setLogger(
//   new opentelemetry.DiagConsoleLogger(),
//   opentelemetry.DiagLogLevel.DEBUG,
// );
module.exports = (serviceName) => {

const exporter = new CollectorTraceExporter({
  // headers: {
  //   foo: 'bar'
  // },
});

const provider = new BasicTracerProvider({
  resource: new Resource({
    [ResourceAttributes.SERVICE_NAME]: serviceName,
  }),
});
provider.addSpanProcessor(new SimpleSpanProcessor(exporter));
provider.addSpanProcessor(new SimpleSpanProcessor(new ConsoleSpanExporter()));
provider.register();

registerInstrumentations({
    // // when boostraping with lerna for testing purposes
    instrumentations: [
      new getNodeAutoInstrumentations(),
    ],
  });

return opentelemetry.trace.getTracer('logzio-collector-exporter-node');

};
