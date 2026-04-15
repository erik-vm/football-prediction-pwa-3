import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  template: `<div class="max-w-lg mx-auto min-h-screen"><router-outlet /></div>`,
  styles: []
})
export class AppComponent {}
