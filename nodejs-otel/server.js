const app = require("express")();
const tracer = require('./tracer.js')('logzio-collector-exporter-node');

const got = require('got');
const PORT = process.env.PORT || 8080;
const DISCOVERY_URL = `http://localhost:${PORT}`;
// This incoming HTTP request should be captured by Trace
app.get('/', async (req, res) => {
  // This outgoing HTTP request should be captured by Trace
  try {
    const {body} = await got(DISCOVERY_URL, {responseType: 'json'});
    const names = body.items.map(item => item.name);
    res.status(200).send(names.join('\n')).end();
    // res.status(200).send(body).end();
  } catch (err) {
    console.error(err);
  }
});
// Start the server

app.listen(PORT, () => {
  console.log(`App listening on port ${PORT}`);
});
