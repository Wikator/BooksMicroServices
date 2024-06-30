Rails.application.routes.draw do
  get 'health/show'
  # Define your application routes per the DSL in https://guides.rubyonrails.org/routing.html

  Rails.application.routes.draw do
  get 'health/show'
    resources :order_items, except: [:new, :edit]
  end


  # Reveal health status on /up that returns 200 if the app boots with no exceptions, otherwise 500.
  # Can be used by load balancers and uptime monitors to verify that the app is live.
  get "healthz" => "rails/health#show", as: :rails_health_check

  # Defines the root path route ("/")
  # root "posts#index"
end
