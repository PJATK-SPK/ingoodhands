import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export enum ScreenSize {
    xs = 0,
    sm = 1,
    md = 2,
    lg = 3,
    xl = 4
}

@Injectable({ providedIn: 'root' })
export class ScreenService {
    private readonly _screenSize = new BehaviorSubject<ScreenSize>(ScreenSize.xs);

    public get screenSize(): ScreenSize { return this._screenSize.value; };
    public get screenSize$(): Observable<ScreenSize> { return this._screenSize.asObservable(); }

    public refresh(): void {
        this.checkScreen();
    }

    private checkScreen(): void {
        const screenWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
        this.updateScreenSize(this._screenSize, screenWidth);
    }

    private updateScreenSize(subject: BehaviorSubject<ScreenSize>, pixels: number): void {
        const spaceSize = this.pixelToSpaceSize(pixels);
        if (spaceSize != subject.value) {
            subject.next(spaceSize);
        }
    }

    private pixelToSpaceSize(pixels: number): ScreenSize {
        let result = ScreenSize.xs;
        if (pixels >= 768) {
            result = ScreenSize.sm;
        }
        if (pixels >= 992) {
            result = ScreenSize.md;
        }
        if (pixels >= 1200) {
            result = ScreenSize.lg;
        }
        if (pixels >= 1600) {
            result = ScreenSize.xl;
        }
        return result;
    }
}