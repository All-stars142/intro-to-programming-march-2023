import { CounterComponent } from './counter.component.cy'

describe('CounterComponent', () => {
  it('should mount', () => {
    cy.mount(CounterComponent)
  })
})