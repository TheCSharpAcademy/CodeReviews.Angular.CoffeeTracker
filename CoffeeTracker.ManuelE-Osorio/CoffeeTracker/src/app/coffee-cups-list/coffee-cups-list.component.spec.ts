import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeCupsListComponent } from './coffee-cups-list.component';

describe('CoffeeCupsListComponent', () => {
  let component: CoffeeCupsListComponent;
  let fixture: ComponentFixture<CoffeeCupsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoffeeCupsListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoffeeCupsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
