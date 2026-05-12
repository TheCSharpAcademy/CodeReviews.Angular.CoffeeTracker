import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CafeFormComponent } from './cafe-form';

describe('CafeFormComponent', () => {
  let component: CafeFormComponent;
  let fixture: ComponentFixture<CafeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CafeFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CafeFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
