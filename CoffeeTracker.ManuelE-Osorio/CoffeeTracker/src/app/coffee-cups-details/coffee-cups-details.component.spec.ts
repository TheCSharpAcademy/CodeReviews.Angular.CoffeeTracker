import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeCupsDetailsComponent } from './coffee-cups-details.component';

describe('CoffeeCupsDetailsComponent', () => {
  let component: CoffeeCupsDetailsComponent;
  let fixture: ComponentFixture<CoffeeCupsDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoffeeCupsDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoffeeCupsDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
