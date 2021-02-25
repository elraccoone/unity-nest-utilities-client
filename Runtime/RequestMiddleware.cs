﻿using System;

namespace ElRaccoone.NestUtilitiesClient {

  /// 
  public abstract class RequestMiddleware {

    ///
    public class Header {

      ///
      internal string name { get; set; } = "";

      ///
      internal string value { get; set; } = "";

      ///
      public Header (string name, string value) {
        this.name = name;
        this.value = value;
      }
    }

    ///
    public virtual Header[] OnGetHeaders () => new Header[] { };

    ///
    public virtual void OnRequestDidCatch (RequestException exception) { }
  }
}