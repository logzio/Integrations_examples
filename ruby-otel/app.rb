require 'opentelemetry/sdk'
require 'opentelemetry/exporter/otlp'
require 'rubygems'
require 'bundler/setup'
Bundler.require
OpenTelemetry::SDK.configure do |c|
 c.service_name = 'ruby-otlp'
 c.use_all
end
get '/' do
 'Hello world!'
end
