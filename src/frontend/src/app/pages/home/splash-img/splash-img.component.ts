import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-splash-img',
  templateUrl: './splash-img.component.html',
  styleUrls: ['./splash-img.component.scss']
})
export class SplashImgComponent {
  @Input() path: string = 'assets/img/man.webp';
  @Input() text: string = '';
}
