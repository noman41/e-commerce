import { Injectable } from '@angular/core';
import { HttpBackend, HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { BaseRequestModel } from '../models/base-request.model';
import { map } from 'rxjs';
import { InterceptorHttpParams } from './http-interceptor.params';
import { environment } from 'src/environments/environment';

@Injectable()
export class RestfulService {

  private baseURL = environment.baseUrl + "api/";
  private httpClient: HttpClient;

  constructor(private http: HttpClient, private handler: HttpBackend) {
    this.httpClient = new HttpClient(handler);
  }

  /* Get Method implementation */
  public GetRequest(url: string, showLoader: boolean = true, useInterceptor: boolean = true, responseType: any = 'json', parameters?: HttpParams) {
    var c = this.baseURL;
    let client: HttpClient;
    if (!useInterceptor) {
      client = this.httpClient;
    }
    else {
      client = this.http;
    }
    const httpParams = new InterceptorHttpParams(showLoader);
    return client.get(this.baseURL + url, {
      observe: 'response',
      params: httpParams,
      responseType: responseType,
      headers: new HttpHeaders({ 'Content-Type': 'application/json'})
    }).pipe(
      map(response => {
        return response.body;
      })
    );
  }

  public GetRequestNoCache(url: string, showLoader: boolean = true, useInterceptor: boolean = true, responseType: any = 'json', parameters?: HttpParams) {
    let client: HttpClient;
    if (!useInterceptor) {
      client = this.httpClient;
    }
    else {
      client = this.http;
    }
    const headers = new HttpHeaders()
      .set('Cache-Control', 'no-cache')
      .set('Pragma', 'no-cache')
      .set('Content-Type', 'application/json');
    const httpParams = new InterceptorHttpParams(showLoader);
    return client.get(url, {
      headers: headers,
      observe: 'response',
      params: httpParams,
      responseType: responseType
    });
  }

  /* Post Method implementation */
  public PostRequest(url: string, body: BaseRequestModel, showLoader: boolean = true, useInterceptor: boolean = true, responseType: any = 'json', parameters?: HttpParams) {
    let client: HttpClient;
    if (!useInterceptor) {
      client = this.httpClient;
    }
    else {
      client = this.http;
    }
    let httpParams = new InterceptorHttpParams(showLoader);
    if (parameters != undefined) {
      httpParams = <InterceptorHttpParams>parameters;
      httpParams.showLoader = showLoader;
    }
    return client.post(this.baseURL + url, JSON.stringify(body), {
      observe: 'response',
      params: httpParams,
      responseType: responseType,
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }).pipe(
      map(response => {
        return response.body;
      })
    );
  }

  /* Put Method implementation */
  public PutRequest(url: string, body: BaseRequestModel, showLoader: boolean = true, useInterceptor: boolean = true, responseType: any = 'json', parameters?: HttpParams) {
    let client: HttpClient;
    if (!useInterceptor) {
      client = this.httpClient;
    }
    else {
      client = this.http;
    }
    const httpParams = new InterceptorHttpParams(showLoader);
    return client.put(this.baseURL + url, JSON.stringify(body), {
      observe: "response",
      params: httpParams,
      responseType: responseType,
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }).pipe(
      map(response => {
        return response.body;
      })
    );
  }

  /* Delete Method implementation */
  public DeleteRequest(url: string, showLoader: boolean = true, useInterceptor: boolean = true, responseType: any = 'json', parameters?: HttpParams) {
    let client: HttpClient;
    if (!useInterceptor) {
      client = this.httpClient;
    }
    else {
      client = this.http;
    }
    const httpParams = new InterceptorHttpParams(showLoader);
    return client.delete(this.baseURL + url, {
      observe: "response",
      params: httpParams,
      responseType: responseType,
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }).pipe(
      map(response => {
        return response.body;
      })
    );
  }
}
