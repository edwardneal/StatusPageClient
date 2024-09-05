# StatusPageClient
API allowing applications to read from statuspage.io's public-facing API

I've written this largely so that I can read from the SendGrid status API, but others also work.

## Known Statuspage.io clients

| Provider | URL | Page ID |
| -------- | --- | ------- |
| SendGrid | https://status.sendgrid.com/api | 3tgl2vf85cht |
| GitHub | https://www.githubstatus.com/api | kctbh9vrtdwd |
| Dropbox | https://status.dropbox.com/api | t34htyd6jblf |

## Other Statuspage.io customers

In the general case, if you know that a status page is provided by Statuspage.io then just add "/api" to the end of the URL. This'll take you to the API summary. You then need to look at the URL given for the *Summary* section. This'll usually be in the format:

`https://3tgl2vf85cht.statuspage.io/api/v2/summary.json`

You want the first part of this URL's domain. In the example above, I'm looking at the SendGrid site and so the Page ID is `3tgl2vf85cht`.

## Using the client

To get started, pass the right Page ID to the constructor of `StatusPageClient` and await its `RefreshAsync` method. This'll get all of the data most people need: component status, ongoing incidents and scheduled maintenance events. If you need historical incidents and scheduled maintenance events, set `RetrieveAllIncidents` and `RetrieveAllMaintenanceEvents` to `true`, respectively.

Once you've called `RefreshAsync`, you can access all of the data by looking at the `StatusPage` property. This provides you with the overall status (`OverallStatus`), some friendly descriptions (`Name` and `Url`), a list of components (`Components`) and the lists of incidents and scheduled maintenance events (`Incidents` and `ScheduledMaintenances`, respectively.)

## Subscriptions

The Statuspage.io API also lets you manage subscribers, so clients can get emails, SMSes and webhook requests as events occur. This functionality is not part of this library.

## Use Cases

Statuspage.io rate limit clients to one request every second. This is usually fine, but if I want to check whether or not SendGrid is working on an incoming HTTP request, I'd quickly run into a rate limit. Instead, I use this library to get the relevant status, store it locally and expose it via health checks.
