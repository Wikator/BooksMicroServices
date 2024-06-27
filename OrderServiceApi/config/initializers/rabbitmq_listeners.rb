Rails.application.config.after_initialize do
  BookChangedListener.perform_later
  BookDeletedListener.perform_later
end