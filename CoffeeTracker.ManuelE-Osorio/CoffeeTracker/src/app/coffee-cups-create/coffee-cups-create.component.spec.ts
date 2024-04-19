import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeCupsCreateComponent } from './coffee-cups-create.component';

describe('CoffeeCupsCreateComponent', () => {
  let component: CoffeeCupsCreateComponent;
  let fixture: ComponentFixture<CoffeeCupsCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoffeeCupsCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoffeeCupsCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
