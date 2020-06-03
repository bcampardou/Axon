import { TranslateLoader } from '@ngx-translate/core';
import { Observable, of, from } from 'rxjs';

export class CustomTranslateLoader implements TranslateLoader {
    getTranslation(lang: string): Observable<any> {
      return from(import(`../../translations/${lang}.json`));
    }
  }
