/**
 * @license
 * Copyright Akveo. All Rights Reserved.
 * Licensed under the MIT License. See License.txt in the project root for license information.
 */
import { Component, OnInit } from '@angular/core';
import { AnalyticsService } from './@core/utils/analytics.service';
import { ApplicationInsightsService } from './pages/extra-components/chat/nca-chat/application-insights.service';

@Component({
  selector: 'ngx-app',
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements OnInit {

  constructor(private analytics: AnalyticsService, private xx: ApplicationInsightsService) {
  }

  ngOnInit(): void {
    this.analytics.trackPageViews();
  }
}
