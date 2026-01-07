FROM node:18-alpine AS build

WORKDIR /app

# This allows Docker to reuse the npm install layer if dependencies haven't changed
COPY ./Asklepios.Web/package*.json ./
# npm ci is designed for automated environments and ensures clean installs
RUN npm ci --only=production=false

# This separates dependency installation from code changes for better caching
COPY ./Asklepios.Web .

RUN npm run build:types || echo "Ignoring TypeScript errors"

ENV NODE_ENV=production
RUN npm run build:prod

RUN rm -rf node_modules

FROM nginx:stable-alpine

LABEL description="Asklepios Web Application"

# Remove default Nginx configuration
RUN rm /etc/nginx/conf.d/default.conf

# Copy custom Nginx configuration
COPY ./Asklepios.Web/nginx.conf /etc/nginx/conf.d/

COPY --from=build /app/dist /usr/share/nginx/html

RUN chown -R nginx:nginx /usr/share/nginx/html && \
    chmod -R 755 /usr/share/nginx/html

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD wget --quiet --tries=1 --spider http://localhost/ || exit 1

# Expose HTTP port
EXPOSE 80

# Start Nginx in foreground mode
CMD ["nginx", "-g", "daemon off;"]